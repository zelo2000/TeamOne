import { Steps } from 'antd';
import React from 'react';

const { Step } = Steps;

class StepsToDo extends React.Component {
  state = {
    current: 0,
  };

  onChange = (current: any) => {
    console.log('onChange:', current);
    this.setState({ current });
  };

  render() {
    const { current } = this.state;
    return (
      <>
        <Steps
          type="navigation"
          current={current}
          onChange={this.onChange}
          className="site-navigation-steps"
        >
          <Step status="finish" title="Step 1" />
          <Step status="process" title="Step 2" />
          <Step status="wait" title="Step 3" />
          <Step status="wait" title="Step 4" />
        </Steps>
      </>
    );
  }
}

export default StepsToDo;
