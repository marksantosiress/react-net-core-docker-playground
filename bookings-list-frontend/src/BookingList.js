import React, { useEffect, useState } from 'react';
import { Table, notification } from 'antd';

const BookingList = () => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const fetchBookings = async () => {
            setLoading(true);
            let response = null;
            try {
                response = await fetch('http://localhost:5000/bookings');
                if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);
                const data = await response.json();
                setBookings(data);
            } catch (error) {
                console.error('Failed to fetch bookings:', error);
                notification.error({
                    message: 'Error fetching bookings',
                    description: `There was an issue fetching the bookings. ${response ? 'Status: ' + response.status : 'Response not received'}`,
                    duration: 4,
                });
            } finally {
                setLoading(false);
            }
        };

        fetchBookings();
    }, []);

    const columns = [
        {
            title: 'Booking ID',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Status',
            dataIndex: 'status',
            key: 'status',
        },
    ];

    return (
        <div style={{ padding: 24 }}>
            <Table dataSource={bookings} columns={columns} loading={loading} />
        </div>
    );
};

export default BookingList;
