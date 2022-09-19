import { Player } from "../models/player";
import { State } from "../models/state";

export function raise(this:State, player:Player, raiseBy:number){
    this.callValue += raiseBy
    this.call(player)
}