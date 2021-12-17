import instanceApi from '../utils/instanceApi'
import { ItemToTakeModel } from '../models/ItemToTakeModel';
import { ItemToTakeBaseModel } from '../models/ItemToTakeBaseModel';

const getByTripId = async (tripId: string): Promise<ItemToTakeModel[]> => {
  return await instanceApi.get(`/itemtotake/${tripId}`);
}

const create = async (tripId: string, itemToTake: ItemToTakeBaseModel): Promise<void> => {
  return await instanceApi.post(`/itemtotake/${tripId}`, itemToTake);
};

const save = async (itemId: string, itemToTake: ItemToTakeBaseModel): Promise<void> => {
  return await instanceApi.put(`/itemtotake/${itemId}`, itemToTake);
};

const remove = async (itemId: string): Promise<void> => {
  return await instanceApi.delete(`/itemtotake/${itemId}`);
}

const updateStatus = async (itemId: string, status: boolean): Promise<void> => {
  return await instanceApi.patch(`/itemtotake/${itemId}/${status}`);
};

const ItemsService = {
  getByTripId,
  create,
  save,
  remove,
  updateStatus
};

export default ItemsService;