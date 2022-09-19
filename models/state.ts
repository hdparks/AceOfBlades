import { Card } from "./card";
import { Player } from "./player";
import { MapArray } from "../utils/mapArray";
import { addPlayer } from "../actions/addPlayer";
import { activateNextPlayer } from "../actions/activateNextPlayer"
import { call } from "../actions/call";
import { raise } from "../actions/raise";

export class State {
    players: MapArray<Player>
    activePlayer?: Player
    deck: Card[]
    community: Card[]
    pot: number
    callValue: number
    done: boolean

    public addPlayer = addPlayer;
    public activateNextPlayer = activateNextPlayer;
    public call = call
    public raise = raise
}

