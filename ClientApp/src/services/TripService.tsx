import instanceApi from "../utils/instanceApi";
import { TripModel } from "../models/TripModel";
import { TripBaseModel } from "../models/TripBaseModel";

const getByUserId = async (userId: string) => {
    return await instanceApi.get<TripModel[]>(`/trip/user/${userId}`);
};

const getById = async (id: string) => {
    return await instanceApi.get<TripModel[]>(`/trip/${id}`);
};

const save = async (id: string, trip: TripBaseModel): Promise<void> => {
    return await instanceApi.put(`/trip/${id}`, trip);
};

const TripService = {
    getByUserId,
    getById,
    save
};

export default TripService;