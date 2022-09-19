import { Card } from "./card";

export type Player = {
    id: string;
    name: string;
    hand: Card[];
    folded: boolean;
    chips: number;
    chipsPlayed: number;
}