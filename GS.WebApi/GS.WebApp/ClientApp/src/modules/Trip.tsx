import React, { FC, useState, useEffect } from 'react';
import { Row, Col, PageHeader } from 'antd';
import { useHistory, useParams } from 'react-router-dom';

import TripDescriptionForm from '../components/TripDescriptionForm';
import ItemsToTake from '../components/ItemsToTake';
import StepsToDo from "../components/StepsToDo";
import { TripBaseModel } from '../models/TripBaseModel';
import { ItemToTakeModel } from '../models/ItemToTakeModel';
import TripService from '../services/TripService';
import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import ToDoService from '../services/ToDoService';
import ItemsService from '../services/ItemsService';
import { NodeStatus } from '../models/NodeStatus';
import { ItemToTakeBaseModel } from '../models/ItemToTakeBaseModel';

interface TripParams {
  tripId: string
};

const Trip: FC = () => {
  const { tripId } = useParams<TripParams>();
  const history = useHistory();

  const [trip, setTrip] = useState<TripBaseModel>();
  const [itemsToTake, setItemsToTake] = useState<ItemToTakeModel[]>([]);
  const [toDoNodes, setToDoNodes] = useState<ToDoNodeModel[]>([]);

  const getTripData = () => {
    TripService.getById(tripId)
      .then((response: any) => {
        setTrip(response.data);
        setItemsToTake(response.data.ItemsToTake);
        setToDoNodes(response.data.ToDoNodes);
      })
      .catch((e: Error) => console.log(e));
  }

  const getToDoNodes = () => {
    ToDoService.getByTripId(tripId)
      .then((response: any) => setToDoNodes(response.data))
      .catch((e: Error) => console.log(e));
  };

  const getItemsToTake = () => {
    ItemsService.getByTripId(tripId)
      .then((response: any) => setItemsToTake(response.data))
      .catch((e: Error) => console.log(e));
  };

  useEffect(getTripData, [tripId]);

  const onTripFormSubmit = (trip: TripBaseModel): void => {
    TripService.save(tripId, trip)
      .then(() => setTrip(trip))
      .catch((e: Error) => console.log(e));
  };

  const onAddToDoNode = (toDoNode: ToDoNodeBaseModel): void => {
    ToDoService.create(tripId, toDoNode)
      .then(() => getToDoNodes())
      .catch((e: Error) => console.log(e));
  };

  const onRemoveToDoNode = (id: string): void => {
    ToDoService.remove(id)
      .then(() => getToDoNodes())
      .catch((e: Error) => console.log(e));
  };

  const onToDoNodeStatusChange = (id: string, status: NodeStatus): void => {
    ToDoService.updateStatus(id, status)
      .then(() => getToDoNodes())
      .catch((e: Error) => console.log(e));
  };

  const onToDoNodeUpdate = (id: string, model: ToDoNodeBaseModel): void => {
    ToDoService.save(id, model)
      .then(() => getToDoNodes())
      .catch((e: Error) => console.log(e));
  };

  const onAddItemToTake = (itemToTake: ItemToTakeBaseModel): void => {
    ItemsService.create(tripId, itemToTake)
      .then(() => getItemsToTake())
      .catch((e: Error) => console.log(e));
  };

  const onRemoveItemToTake = (id: string): void => {
    ItemsService.remove(id)
      .then(() => getItemsToTake())
      .catch((e: Error) => console.log(e));
  };

  const onItemToTakeStatusChange = (id: string, status: boolean): void => {
    ItemsService.updateStatus(id, status)
      .then(() => getItemsToTake())
      .catch((e: Error) => console.log(e));
  };

  const onItemToTakeUpdate = (id: string, model: ItemToTakeBaseModel): void => {
    ItemsService.save(id, model)
      .then(() => getItemsToTake())
      .catch((e: Error) => console.log(e));
  };

  const onAddClicked = () => {
    onAddItemToTake({ name : "New Item" });
  };

  return (
    <Row className="trip-content">
      <Col span={24}>
        <PageHeader onBack={() => history.push('/trips')} title={trip?.name || "New Trip"} />
      </Col>
      <Col span={24}>
        <Row justify="center">
          <Col 
            xs={{ span: 22, offset: 0 }}
            sm={{ span: 18, offset: 0 }}
            md={{ span: 14, offset: 0 }}
            lg={{ span: 11, offset: 0 }}
            className="trip-form-container">
            {trip ? <TripDescriptionForm trip={trip as TripBaseModel} onSubmit={onTripFormSubmit} /> : <></>}
          </Col>
          <Col
            xs={{ span: 22, offset: 0 }}
            sm={{ span: 18, offset: 0 }}
            md={{ span: 16, offset: 0 }}
            lg={{ span: 14, offset: 0 }}
            className="items-container">
            {trip ? 
            <ItemsToTake 
              items={(itemsToTake || [])}
              onAddItemToTake={onAddClicked}
              onRemoveItemToTake={onRemoveItemToTake}
              onItemToTakeStatusChange={onItemToTakeStatusChange}
              onItemToTakeUpdate={onItemToTakeUpdate}
            /> : <></>}
          </Col>
          <Col xs={24} sm={22} md={20} lg={18} className="todo-container">
            <StepsToDo
              items={toDoNodes}
              trip={trip as TripBaseModel}
              onAddToDoNode={onAddToDoNode}
              onRemoveToDoNode={onRemoveToDoNode}
              onToDoNodeStatusChange={onToDoNodeStatusChange}
              onToDoNodeUpdate={onToDoNodeUpdate}
            />
          </Col>
        </Row>
      </Col>
    </Row >
  );
};

export default Trip;