import { User } from "./Users";

export class TableUrl{
    id: String;
    fullURL: String;
    shortenedURL: String;
    creationDate: Date;
}

export class BaseUrl{
    id: String;
    originalURL: String;
    shortenedURL: String;
    creationDate: Date;
    user: User;
}