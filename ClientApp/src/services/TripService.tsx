import instanceApi from "../utils/instanceApi";
import { TripModel } from "../models/TripModel";

const getByUserId = (userId: string) => {
    return instanceApi.get<TripModel[]>(`/trip/user/${userId}`)
};

const getTripById = (id: string) => {
    return instanceApi.get<TripModel[]>(`/trip/${id}`)
};

const TripService = {
    getByUserId,
    getTripById
};

export default TripService;