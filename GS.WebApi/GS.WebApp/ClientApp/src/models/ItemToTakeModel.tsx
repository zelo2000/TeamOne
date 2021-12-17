import { ItemToTakeBaseModel } from "./ItemToTakeBaseModel";

export interface ItemToTakeModel extends ItemToTakeBaseModel {
  id: string;
  isTaken: boolean;
}
