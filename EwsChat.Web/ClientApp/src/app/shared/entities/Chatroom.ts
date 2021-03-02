import { ChatUser } from "./Chatuser";

export interface ChatRoom {
    id: number;
    name: number;
    participants: ChatUser[];  
}