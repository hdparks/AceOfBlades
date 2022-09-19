import { State } from "../models/state";
import { ring } from "../utils/ring";

export function activateNextPlayer(this:State){
    let activeIndex = this.activePlayer == null? 0 : this.players.items.indexOf(this.activePlayer)

    //  Find next non-folded player
    for (let player of ring(this.players.items, activeIndex)){
        if (!player.folded){
            this.activePlayer = player
            return
        }
    }
}