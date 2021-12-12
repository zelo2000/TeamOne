import { Steps } from 'antd';
import React, { FC } from 'react';
import StepTimeline from './StepTimeline';
import { TimelineColorType } from '../models/TimelineColorType';

const { Step } = Steps;

interface TripStepData {
  status: 'wait' | 'process' | 'finish' | 'error';
  items: string[];
  color: string;
}

export interface StepsToDoProps {
  before: TripStepData;
  during: TripStepData;
  after: TripStepData;
}

const StepsToDo: FC<StepsToDoProps> = ({ before, during, after }: StepsToDoProps) => {
  const [ current, setCurrent ] = React.useState(0);

  const onChange = (current: any) => {
    console.log('onChange:', current);
    setCurrent(current);
  };

  const currentItems = () => {
    if (current === 0)
      return before.items;
    else if (current === 1)
      return during.items;
    else if (current === 2)
      return after.items;
    else
      return []
  }

  return (
    <>
      <Steps
        type="navigation"
        current={current}
        onChange={onChange}
        className="site-navigation-steps"
      >
        <Step key="before" status={before.status} title="Before"/>
        <Step key="during" status={during.status} title="During"/>
        <Step key="after" status={after.status} title="After"/>
      </Steps>
      <div className='steps-content'>
        <StepTimeline items={currentItems()} color={TimelineColorType[current]}/>
      </div>
    </>
  );
};

export default StepsToDo;
