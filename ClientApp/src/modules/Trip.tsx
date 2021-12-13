import React, { FC, useState, useEffect } from 'react';
import { Row, Col } from 'antd';
import { useParams } from 'react-router-dom';

import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo, { StepsToDoProps } from "../components/StepsToDo";
import { TimelineColorType } from '../models/TimelineColorType';
import { TripBaseModel } from '../models/TripBaseModel';
import { ItemToTakeModel } from '../models/ItemToTakeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import TripService from '../services/TripService';

const data: StepsToDoProps = {
  before: {
    status: "finish",
    items: ["item1", "item2"],
    color: TimelineColorType[1]
  },
  during: {
    status: "process",
    items: ["item1", "item2", "item3", "item4"],
    color: TimelineColorType[2]
  },
  after: {
    status: "wait",
    items: ["item1"],
    color: TimelineColorType[3]
  }
}

interface TripParams {
  tripId: string
};

const Trip: FC = () => {
  const { tripId } = useParams<TripParams>();
  const [trip, setTrip] = useState<TripBaseModel>();
  const [itemsToTake, setItemsToTake] = useState<ItemToTakeModel[]>();
  const [toDoNodes, setToDoNodes] = useState<ToDoNodeBaseModel[]>();

  useEffect(() => {
    TripService.getById(tripId)
      .then((response: any) => {
        setTrip(response.data[0]);
        setItemsToTake(response.data[0].ItemsToTake);
        setToDoNodes(response.data[0].ToDoNodes);
      })
      .catch((e: Error) => {
        console.log(e)
      });
  }, []);

  const onTripFormSubmit = (trip: TripBaseModel): void => {
    TripService.save(tripId, trip)
    .then(() => {
      console.log("success");
      setTrip(trip);
    })
    .catch((e: Error) => {
      console.log(e);
    });
  };

  return (
    <Row justify="center" className="trip-content">
      <Col xs={22} sm={18} md={14} lg={10}>
        {trip ? <TripDescriptionForm trip={trip as TripBaseModel} onSubmit={onTripFormSubmit}/> : <></>}
      </Col>
      <Col xs={24} sm={22} md={20} lg={18}>
        <StepsToDo {...data}/>
      </Col>
    </Row>
  );
};

export default Trip;