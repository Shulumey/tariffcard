/* eslint-disable prefer-arrow/prefer-arrow-functions */
export interface SnackMessage {
    duration: number;
    severity: "info" | "warn" | "error";
    text: string;
}

export function getMessageDuration(severity: "info" | "warn" | "error"): number {
    switch (severity) {
        case "info": return 3 * 1000;
        case "warn": return 15 * 1000;
        case "error": return 60 * 1000;
    }

    const exhaustiveCheck: never = severity;
}
