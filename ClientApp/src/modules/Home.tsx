import React, { FC, useEffect, useState } from 'react';
import { Row, Col } from 'antd';
import TripService from '../services/TripService';
import { TripModel } from '../models/TripModel';
import TripsCardList from "../components/TripsCardList";

const Home: FC = () => {
  const [trips, setTrips] = useState([] as TripModel[]);

  useEffect(() => {
    TripService.getByUserId("a3ce5e17-35eb-4c1d-a6fe-565ddd67316c")
      .then((response: any) => {
        setTrips(response.data);
        console.log(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  }, []);
  
  return (
    <Row justify="center" className="trip-content">
      <Col xs={24} sm={22} md={20} lg={18}>
        <TripsCardList trips={trips}/>
      </Col>
    </Row>
  );
};

export default Home;
