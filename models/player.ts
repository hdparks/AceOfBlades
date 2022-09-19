import { Card } from "./card";

export type Player = {
    id: string;
    name: string;
    hand: Card[];
    active: boolean;
    folded: boolean;
    chips: number;
    chipsPlayed: number;
}