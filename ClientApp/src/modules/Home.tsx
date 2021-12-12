import React, { FC } from 'react';
import { Button } from 'antd';
import { Link } from 'react-router-dom';
import TripService from '../services/TripService';

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
      <Button type="primary" onClick={onClick}>
        <Link to="/trip/5aaed057-6794-4f7a-812c-754d8640e9bb" key="5aaed057-6794-4f7a-812c-754d8640e9bb">Button</Link>
      </Button>
    </div>
  );
};

export default Home;
