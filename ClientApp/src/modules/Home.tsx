import React, { FC, useEffect, useState } from 'react';
import { Row, Col, Typography } from 'antd';
import TripService from '../services/TripService';
import { TripModel } from '../models/TripModel';
import { TripStatus } from '../models/TripStatus';
import TripsCardList from "../components/TripsCardList";

const { Title } = Typography;

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
    <>
      <Row justify="center" className="trip-content">
        <Col xs={24} sm={22} md={20} lg={18}>
          <Title>In progress</Title>
          <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.InProgress)}/>
        </Col>
      </Row>
        <Row justify="center" className="trip-content">
        <Col xs={24} sm={22} md={20} lg={18}>
          <Title>Planned</Title>
          <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.Planned)}/>
        </Col>
      </Row>
      <Row justify="center" className="trip-content">
        <Col xs={24} sm={22} md={20} lg={18}>
          <Title>Finished</Title>
          <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.Closed)}/>
        </Col>
      </Row>
    </>
  );
};

export default Home;
