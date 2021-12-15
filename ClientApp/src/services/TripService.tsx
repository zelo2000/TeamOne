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

const create = async (trip: TripBaseModel): Promise<void> => {
    return await instanceApi.post('/trip', trip);
};

const remove = async (id: string): Promise<void> => {
    return await instanceApi.delete(`/trip/${id}`);
};

const TripService = {
    getByUserId,
    getById,
    save,
    create,
    remove
};

export default TripService;