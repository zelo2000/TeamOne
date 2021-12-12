import React, { FC } from 'react';
import { Card, Col } from 'antd';
import { Link } from 'react-router-dom';

import { TripModel } from '../models/TripModel';
import moment from 'moment';

const { Meta } = Card;

interface CardListProps {
  trips: TripModel[]
};

function isSomeValueDefined(someValue: any): any {
  if (!!someValue) {
    return someValue
  }
}

const TripsCardList: FC<CardListProps> = ({ trips }: CardListProps) => {
  return (
      <Col xs={12} sm={8} md={8} lg={6}>
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
                  description={isSomeValueDefined(trip.Description)}
                />
                {moment(isSomeValueDefined(trip.StartDate)).format("MMM Do YY") + " - " +
                  moment(isSomeValueDefined(trip.EndDate)).format("MMM Do YY")}   
              </Card>
            </Link>
          );
        })}
      </Col>
  );
};

export default TripsCardList;