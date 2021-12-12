import React, { FC } from 'react';
import { Button } from 'antd';
import TripService from '../services/TripService'
import { TripModel } from '../models/TripModel';

const Home: FC = () => {
  const onClick = () => {
    TripService.getByUserId("a3ce5e17-35eb-4c1d-a6fe-565ddd67316c")
      .then((response: any) => {
        console.log(response);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };

  return (
    <div className="App">
      <Button type="primary" onClick={onClick}>Button</Button>
    </div>
  );
};

export default Home;
