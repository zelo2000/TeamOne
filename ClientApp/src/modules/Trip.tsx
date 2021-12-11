import React, { FC } from 'react';
import { Row, Col } from 'antd';
import TripDescriptionForm from '../components/TripDescriptionForm';
import StepsToDo, { StepsToDoProps } from "../components/StepsToDo";


const data: StepsToDoProps = {
  before: {
    status: "finish",
    items: ["item1", "item2"]
  },
  during: {
    status: "process",
    items: ["item1", "item2", "item3", "item4"]
  },
  after: {
    status: "wait",
    items: ["item1"]
  }
}

const Trip: FC = () => {
  return (
    <Row>
      <Col span={24}>
        <TripDescriptionForm name="Some trip" description="Some description"/>
      </Col>
      <Col span={24}>
        <StepsToDo {...data}/>
      </Col>
    </Row>
  );
};

export default Trip;