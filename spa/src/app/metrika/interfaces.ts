/* eslint-disable @typescript-eslint/naming-convention */
export interface ICallbackOptions {
    callback?: () => any;
    ctx?: any;
}

export interface ICommonOptions extends ICallbackOptions {
    params?: any;
}

export interface IMetrikaGoalEventOptions {
    target: string;
    options?: ICommonOptions;
}

export interface IMetrikaHitOptions extends ICommonOptions {
    title?: any;
    referer?: string;
    PlayerID?: number;
}

export interface IMetrikaHitEventOptions {
    url: string;
    hitOptions?: IMetrikaHitOptions;
}
