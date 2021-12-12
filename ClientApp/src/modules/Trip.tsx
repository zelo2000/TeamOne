import React, { FC, useState, useEffect } from 'react';
import { Row, Col } from 'antd';
import { useParams } from 'react-router-dom';

import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo, { StepsToDoProps } from "../components/StepsToDo";
import { TimelineColorType } from '../models/TimelineColorType';
import { TripModel } from '../models/TripModel';
import { TripBaseModel } from '../models/TripBaseModel';
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
  const [trip, setTrip] = useState<TripModel>();

  useEffect(() => {
    TripService.getById(tripId)
      .then((response: any) => {
        setTrip(response.data[0])
      })
      .catch((e: Error) => {
        console.log(e)
      });
  }, []);

  return (
    <Row justify="center" className="trip-content">
      <Col xs={22} sm={18} md={14} lg={10}>
        {trip ? <TripDescriptionForm {...trip as TripBaseModel}/> : <></>}
      </Col>
      <Col xs={24} sm={22} md={20} lg={18}>
        <StepsToDo {...data}/>
      </Col>
    </Row>
  );
};

export default Trip;