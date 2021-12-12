import React, { FC } from 'react';
import { Card, Col, Row } from 'antd';
import { Link } from 'react-router-dom';

import { TripModel } from '../models/TripModel';

const { Meta } = Card;

interface CardListProps {
  trips: TripModel[]
};

const TripsCardList: FC<CardListProps> = ({ trips }: CardListProps) => {
  return (
    <div className="site-card-wrapper">
    <Row gutter={16}>
      <Col span={8}>
        {trips.map((trip) => {
          return (
            <Link key={trip.Id} to={`trip/${trip.Id}`}>
              <Card 
                bordered={false} 
                hoverable={true}
                cover={<div className="trip-img"></div>}
              >
                <Meta
                  title={trip.Name}
                  description={trip.Description}
                />
              </Card>
            </Link>
          );
        })}
      </Col>
    </Row>
  </div>
  );
};

export default TripsCardList;