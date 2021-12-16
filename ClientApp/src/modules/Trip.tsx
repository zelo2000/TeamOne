import React, { FC, useState, useEffect } from 'react';
import { Row, Col, PageHeader } from 'antd';
import { useHistory, useParams } from 'react-router-dom';

import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo from "../components/StepsToDo";
import { TripBaseModel } from '../models/TripBaseModel';
import { ItemToTakeModel } from '../models/ItemToTakeModel';
import TripService from '../services/TripService';
import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import ToDoService from '../services/ToDoService';
import { NodeStatus } from '../models/NodeStatus';

interface TripParams {
  tripId: string
};

const Trip: FC = () => {
  const { tripId } = useParams<TripParams>();
  const history = useHistory();

  const [trip, setTrip] = useState<TripBaseModel>();
  const [itemsToTake, setItemsToTake] = useState<ItemToTakeModel[]>();
  const [toDoNodes, setToDoNodes] = useState<ToDoNodeModel[]>([]);

  const getTripData = () => {
    TripService.getById(tripId)
      .then((response: any) => {
        setTrip(response.data[0]);
        setItemsToTake(response.data[0].ItemsToTake);
        setToDoNodes(response.data[0].ToDoNodes);
      })
      .catch((e: Error) => console.log(e));
  }

  const getToDoNodes = () => {
    ToDoService.getByTripId(tripId)
      .then((response: any) => setToDoNodes(response.data))
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

  return (
    <Row className="trip-content">
      <Col span={24}>
        <PageHeader onBack={() => history.push('/trips')} title={trip?.name || "New Trip"} />
      </Col>
      <Col span={24}>
        <Row justify="center">
          <Col xs={22} sm={18} md={14} lg={10} className="trip-form-container">
            {trip ? <TripDescriptionForm trip={trip as TripBaseModel} onSubmit={onTripFormSubmit} /> : <></>}
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