import React, { FC, useEffect, useState, useCallback } from 'react';
import { Card, Row, Col, Typography, Button } from 'antd';
import TripService from '../services/TripService';
import { TripModel } from '../models/TripModel';
import { TripStatus } from '../models/TripStatus';
import TripsCardList from "../components/TripsCardList";
import { GetAuthData } from '../utils/storage-helper';

const { Title } = Typography;

const Trips: FC = () => {
  const [trips, setTrips] = useState<TripModel[]>([]);
  const authData = GetAuthData();
  const userId = authData?.id === undefined ? "" : authData.id;

  const loadTrips = useCallback((userId: string) => {
    TripService.getByUserId(userId)
      .then((response: { data: TripModel[] }) => {
        setTrips(response.data);
        console.log(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  }, [setTrips]);

  useEffect(() => {
    loadTrips(userId);
  }, [loadTrips, userId]);

  const createTrip = useCallback(() => {
    TripService.create({ userId: userId })
      .then(() => {
        loadTrips(userId);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  }, [loadTrips, userId]);

  const deleteTrip = useCallback((tripId: string) => {
    TripService.remove(tripId)
      .then(() => {
        console.log("success");
        loadTrips(userId);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  }, [loadTrips, userId]);

  return (
    <>
      <Row gutter={[0, 12]} justify="center" className="trip-content">
        <Col xs={24} sm={24} md={22} lg={20}>
          <Title level={3} underline>In progress</Title>
          <div className="site-card-wrapper">
            <Row gutter={[16, 16]}>
              <TripsCardList trips={trips.filter(trip => trip.status === TripStatus.InProgress)}
                onDelete={deleteTrip} />
            </Row>
          </div>
        </Col>
        <Col xs={24} sm={24} md={22} lg={20}>
          <Title level={3} underline>Planned</Title>
          <div className="site-card-wrapper">
            <Row gutter={[16, 16]}>
              <TripsCardList trips={trips.filter(trip => trip.status === TripStatus.Planned)}
                onDelete={deleteTrip} />
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
              <TripsCardList trips={trips.filter(trip => trip.status === TripStatus.Closed)}
                onDelete={deleteTrip} />
            </Row>
          </div>
        </Col>
      </Row>
    </>
  );
};

export default Trips;
