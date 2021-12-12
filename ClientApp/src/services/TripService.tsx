import instanceApi from "../utils/instanceApi";
import { TripModel } from "../models/TripModel";

const getByUserId = (userId: string) => {
    return instanceApi.get<TripModel[]>(`/trip/user/${userId}`)
};

const TripService = {
    getByUserId
};

export default TripService;