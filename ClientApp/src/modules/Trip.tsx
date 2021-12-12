import React, { FC } from 'react';
import { Row, Col } from 'antd';
import { useParams } from 'react-router-dom';
import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo, { StepsToDoProps } from "../components/StepsToDo";
import { TimelineColorType } from '../models/TimelineColorType';

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

const Trip: FC = () => {
  const params = useParams();
  
  return (
    <Row justify="center" className="trip-content">
      <Col xs={22} sm={18} md={14} lg={10}>
        <TripDescriptionForm name="Some trip" description="Some description"/>
      </Col>
      <Col xs={24} sm={22} md={20} lg={18}>
        <StepsToDo {...data}/>
      </Col>
    </Row>
  );
};

export default Trip;