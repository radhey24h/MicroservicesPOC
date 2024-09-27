import React, { useState } from 'react';
import axios from 'axios';

interface User {
    name: string;
    email: string;
    location: string;
    phoneNumber: string;
    userType: string;
}

const UserForm: React.FC = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [location, setLocation] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [userType, setUserType] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);

        const user: User = {
            name,
            email,
            location,
            phoneNumber,
            userType,
        };

        try {
            await axios.post('/api/users', user);
            setLoading(false);
            // You can reset the form here if needed
            setName('');
            setEmail('');
            setLocation('');
            setPhoneNumber('');
            setUserType('');
        } catch (err) {
            setError('Failed to submit the form. Please try again.');
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="mb-3">
                <input type="text" className="form-control" placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} />
            </div>
            <div className="mb-3">
                <input type="email" className="form-control" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
            </div>
            <div className="mb-3">
                <input type="tel" className="form-control" placeholder="Phone Number" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
            </div>
            <div className="mb-3">
                <select className="form-select" value={userType} onChange={(e) => setUserType(e.target.value)}>
                    <option value="Student">I am a student</option>
                    <option value="Professional">I am a professional</option>
                </select>
            </div>
            <div className="mb-3">
                <input type="text" className="form-control" placeholder="Location" value={location} onChange={(e) => setLocation(e.target.value)} />
            </div>

            <button type="submit" className="btn btn-primary" disabled={loading}>
                {loading ? 'Submitting...' : 'Submit'}
            </button>
            {error && <p className="mt-3 text-danger">{error}</p>}
        </form>
    );
};

export default UserForm;
