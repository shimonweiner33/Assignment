export interface Rooms {
    rooms: Room[];
}

export interface Room {
    roomNum: number;
    roomName: string;
    userNames: string[];
}