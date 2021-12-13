import React, { FC, useEffect, useState } from 'react';
import { Card, Row, Col, Typography, Button, Divider  } from 'antd';
import TripService from '../services/TripService';
import { TripModel } from '../models/TripModel';
import { TripStatus } from '../models/TripStatus';
import TripsCardList from "../components/TripsCardList";

const { Title } = Typography;

const Home: FC = () => {
  const [trips, setTrips] = useState([] as TripModel[]);

  const userId = "a3ce5e17-35eb-4c1d-a6fe-565ddd67316c";

  const loadTrips = (userId: string) => {
    TripService.getByUserId(userId)
    .then((response: any) => {
      setTrips(response.data);
      console.log(response.data);
    })
    .catch((e: Error) => {
      console.log(e);
    });
  }

  useEffect(() => {
    loadTrips(userId);
  }, []);

  const createTrip = () => {
    TripService.create({UserId: userId})
      .then(() => {
        loadTrips(userId);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };
  
  return (
    <>
      <Row gutter={[0, 12]} justify="center" className="trip-content">
        <Col xs={24} sm={24} md={22} lg={20}>
            <Title level={3} underline>In progress</Title>
            <div className="site-card-wrapper">
              <Row gutter={[16, 16]}> 
                <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.InProgress)}/>
              </Row>
            </div>
          </Col>
          <Col xs={24} sm={24} md={22} lg={20}>
            <Title level={3} underline>Planned</Title>
            <div className="site-card-wrapper">
              <Row gutter={[16, 16]}> 
                <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.Planned)}/>
                <Col xs={12} sm={8} md={8} lg={6}>
                <Card 
                  bordered={false} 
                  hoverable={true}
                  cover={<div className="trip-img"></div>}
                  onClick={createTrip}
                  >
                  <Row justify="center" className="add-trip-button">
                    <Button type="primary">
                      Add trip
                    </Button>
                  </Row>
                </Card>
                </Col>
              </Row>
            </div>
          </Col>
          <Col xs={24} sm={24} md={22} lg={20}>
            <Title level={3} underline>Finished</Title>
            <div className="site-card-wrapper">
              <Row gutter={[16, 16]}> 
                <TripsCardList trips={trips.filter(trip => trip.Status === TripStatus.Closed)}/>
              </Row>
            </div>
          </Col>
      </Row>
    </>
  );
};

export default Home;
