import { Player } from "../models/player";
import { State } from "../models/state";

export function call(this:State, player:Player){
    //  Figure out how much the call is worth
    let call = this.callValue - player.chipsPlayed
    
    //  Transact
    player.chips -= call
    this.pot += call

    //  TODO: what if there aren't enough chips?
    
}