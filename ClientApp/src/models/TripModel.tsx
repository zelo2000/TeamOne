import { TripBaseModel } from "./TripBaseModel";
import { TripStatus } from "./TripStatus";
import { ItemToTakeModel } from "./ItemToTakeModel";
import { ToDoNodeModel } from "./ToDoNodeModel";


export interface TripModel extends TripBaseModel {
  id: string;
  status: TripStatus;
  itemsToTake: ItemToTakeModel[];
  toDoNodes: ToDoNodeModel[];
}
