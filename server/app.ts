import { Player } from "../models/player";

const express = require('express');
const app = express();
const http = require('http');
const server = http.createServer(app);
const { Server } = require("socket.io")
import type { Socket } from 'socket.io'
const io = new Server(server)

const PORT = 5000

let players:MapArray<Player> = new MapArray<Player>();


app.get('/', (req, res) => {
    res.sendFile(__dirname + '/index.html');
});

io.on('connection', (socket:Socket) => {
    //  create a new player instance
    let player: Player = {
        id:socket.id,
        name:'',
        hand:[],
        active:players.items.length == 0,
        folded:false,
        chips:100,
        chipsPlayed:0
    }
    //  track new player
    players.push(player, player.id)

    socket.on('disconnect',() => {
        //  Remove from players list
        players.remove(player.id)
        console.log('a user disconnected')
    })

    socket.on('fold',() => {
        player.folded = true
        //  Handle transition
    })

    socket.on('call',() => {
        // TODO: Handle call
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