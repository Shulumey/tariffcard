/* eslint-disable @typescript-eslint/naming-convention */
export interface IGoogleTagManagerConfig {
    id: string | null;
    gtm_auth?: string;
    gtm_preview?: string;
    [key: string]: string | null | undefined;
}
