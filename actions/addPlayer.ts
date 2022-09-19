import { Player } from "../models/player";
import { State } from "../models/state";

export function addPlayer(this:State, player:Player){
    this.players.push(player, player.id)
    
    if (this.activePlayer == null){
        this.activePlayer = player
    }
}