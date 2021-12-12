import React, { FC, useState, useEffect } from 'react';
import { Row, Col } from 'antd';
import { useParams } from 'react-router-dom';

import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo, { StepsToDoProps } from "../components/StepsToDo";
import { TimelineColorType } from '../models/TimelineColorType';
import { TripModel } from '../models/TripModel';
import TripService from '../services/TripService';

const data: StepsToDoProps = {
  before: {
    status: "finish",
    items: ["item1", "item2"],
    color: TimelineColorType.Before
  },
  during: {
    status: "process",
    items: ["item1", "item2", "item3", "item4"],
    color: TimelineColorType.During
  },
  after: {
    status: "wait",
    items: ["item1"],
    color: TimelineColorType.After
  }
}

interface TripParams {
  tripId: string
};

const Trip: FC = () => {
  const { tripId } = useParams<TripParams>();
  const [trip, setTrip] = useState<TripModel>();

  useEffect(() => {
    TripService.getTripById(tripId)
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
        {trip ? <TripDescriptionForm name={trip.Name} description={trip.Description}/> : <></>}
      </Col>
      <Col xs={24} sm={22} md={20} lg={18}>
        <StepsToDo {...data}/>
      </Col>
    </Row>
  );
};

export default Trip;