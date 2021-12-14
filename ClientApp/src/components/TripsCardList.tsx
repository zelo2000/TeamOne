import React, { FC } from 'react';
import { Button, Card, Col, Modal, Row } from 'antd';
import { Link } from 'react-router-dom';

import { TripModel } from '../models/TripModel';
import moment from 'moment';
import { ExclamationCircleOutlined } from '@ant-design/icons';

const { Meta } = Card;
const { confirm } = Modal;

interface CardListProps {
  trips: TripModel[];
  onDelete: (id: string) => void;
};

function isSomeValueDefined(someValue: any): any {
  if (!!someValue) {
    return someValue
  }
}

const TripsCardList: FC<CardListProps> = ({ trips, onDelete }: CardListProps) => {
  const showConfirm = (e: any , description : any, id: any) => {
    e.preventDefault();
    confirm({
      title: 'Do you want to delete this trip?',
      closable: true,
      maskClosable: true,
      icon: <ExclamationCircleOutlined />,
      content: description,
      onOk() {
        onDelete(id);
      },
    });
  }
  
  return (
      <>
      {trips.map((trip) => {
          return (
            <Col key={trip.Id} xs={12} sm={8} md={8} lg={6}>
              <Link to={`trip/${trip.Id}`}>
                <Card 
                    bordered={false} 
                    hoverable={true}
                    cover={<div className="trip-img"></div>}
                >
                    <Meta
                      title={trip.Name}
                      description=
                      {<span className="trip-card-desc">
                            {isSomeValueDefined(trip.Description)}
                      </span>}
                    />
                    {(trip.StartDate != null && trip.EndDate != null) ?
                      moment(isSomeValueDefined(trip.StartDate)).format("MMM Do YY") + " - " +
                      moment(isSomeValueDefined(trip.EndDate)).format("MMM Do YY") : ""}
                    <Row justify="center" className="delete-trip-button">
                      <Button type="primary" danger onClick={(e) => showConfirm(e, trip.Description, trip.Id)}>
                        Delete trip
                      </Button>
                    </Row>
                </Card>
              </Link>
            </Col>
          );
      })}
    </>
  );
};

export default TripsCardList;