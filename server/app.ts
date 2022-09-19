import { Player } from "../models/player";
import { State } from "../models/state";
const express = require('express');
const app = express();
const http = require('http');
const server = http.createServer(app);
const { Server } = require("socket.io")
import type { Socket } from 'socket.io'
const io = new Server(server)
const PORT = 5000

const state = new State()


io.on('connection', (socket:Socket) => {
    //  create a new player instance
    let player: Player = {
        id:socket.id,
        name:'',
        hand:[],
        folded:false,
        chips:100,
        chipsPlayed:0
    }

    state.addPlayer(player)

    socket.on('disconnect',() => {
        console.log('a user disconnected')
    });

    socket.on('fold',() => {
        //  Validate
        if (state.activePlayer != player){
            console.log("Not your turn!")
            return
        }

        //  Mark as folded
        player.folded = true

        //  Handle transition
        state.activateNextPlayer()
    });

    socket.on('call',() => {
        if (state.activePlayer != player){
            console.log("Not your turn")
            return
        }

        state.call(player)
        state.activateNextPlayer()   
    });

    socket.on('raise',(raiseBy) => {
        if (state.activePlayer != player){
            console.log("Not your turn")
            return
        }

        state.raise(player, raiseBy)
        state.activateNextPlayer()
    })

    for(let messageType of ['fold','raise','call']){
        socket.on(messageType, () => {
            console.log('message: ' + messageType);
            io.emit('chat message', messageType)
        });
    }
});

server.listen(PORT,() => {
    console.log(`listening on *:${PORT}`);
});