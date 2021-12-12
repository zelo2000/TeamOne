import React, { FC, useEffect, useState } from 'react';
import { Row, Col, Button } from 'antd';
import { Link } from 'react-router-dom';
import TripService from '../services/TripService';
import CardList from "../components/CardList";

const Home: FC = () => {
  const [trips, setTrips] = useState();

  useEffect(() => {
    TripService.getByUserId("a3ce5e17-35eb-4c1d-a6fe-565ddd67316c")
      .then((response: any) => {
        setTrips(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  }, []);
  
  return (
    <Row justify="center" className="trip-content">
      <Col xs={24} sm={22} md={20} lg={18}>
        <CardList/>
        <Button type="primary" onClick={() => useEffect}>
          <Link 
            to="/trip/5aaed057-6794-4f7a-812c-754d8640e9bb" 
            key="5aaed057-6794-4f7a-812c-754d8640e9bb"
          >
            Button
          </Link>
        </Button>
      </Col>
    </Row>
  );
};

export default Home;
