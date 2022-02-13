import { Car } from "./car";

export interface carWithUserDetails {
    car: Car,
    userId: string,
    userName: string
}