import { ConnectionPositionPair, OriginConnectionPosition, OverlayConnectionPosition } from "@angular/cdk/overlay";
import { HorizontalAttachment, Position, VerticalAttachment } from "../tooltip";

export class ParametrizedConnectionPosition extends ConnectionPositionPair {
    public position: Position;
    public horizontalAttachment: HorizontalAttachment;
    public verticalAttachment: VerticalAttachment;

    constructor(parameters: {
        origin: OriginConnectionPosition;
        overlay: OverlayConnectionPosition;
        offsetX?: number | undefined;
        offsetY?: number | undefined;
        position: Position;
        horizontalAttachment: HorizontalAttachment;
        verticalAttachment: VerticalAttachment;
    }) {
        super(parameters.origin, parameters.overlay, parameters.offsetX, parameters.offsetY);
        this.position = parameters.position;
        this.horizontalAttachment = parameters.horizontalAttachment;
        this.verticalAttachment = parameters.verticalAttachment;
    }
}
