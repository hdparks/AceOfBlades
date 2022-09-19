import { Card } from "./card";
import { Player } from "./player";

export type State = {
    players: Player[],
    deck: Card[],
    community: Card[],
    pot: number,
    callValue: number,
    done: boolean
}

