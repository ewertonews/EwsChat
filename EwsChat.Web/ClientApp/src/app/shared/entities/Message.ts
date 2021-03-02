import { BrowserDynamicTestingModule } from "@angular/platform-browser-dynamic/testing";

export interface Message{
    id: string,
    targetRoomId: number;
    createdAtString: string;        
    text: string;
    toUserName: string;
}