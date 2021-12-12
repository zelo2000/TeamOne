import { TripBaseModel } from "./TripBaseModel";
import { TripStatus } from "./TripStatus";
import { ItemToTakeModel } from "./ItemToTakeModel";
import { ToDoNodeModel } from "./ToDoNodeModel";


export interface TripModel extends TripBaseModel {
  Id: string;
  Status: TripStatus;
  ItemsToTake: ItemToTakeModel[];
  ToDoNodes: ToDoNodeModel[];
}
