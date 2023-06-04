import { ConnectionPositionPair } from "@angular/cdk/overlay";
import { Position } from "../tooltip";

export interface IConnectionPosition {
    position: ConnectionPositionPair;
    fallbackPositions: Position[];
}
