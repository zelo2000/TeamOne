import { ItemToTakeBaseModel } from "./ItemToTakeBaseModel";

export interface ItemToTakeModel extends ItemToTakeBaseModel {
  Id: string;
  IsTaken: boolean;
}
