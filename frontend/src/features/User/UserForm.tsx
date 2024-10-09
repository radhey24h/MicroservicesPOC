import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface User {
    name: string;
    email: string;
    subject: string;
    phoneNumber: string;
    userType: string;
}

const UserForm: React.FC = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [subject, setSubject] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [userType, setUserType] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleClickOutside = (e: MouseEvent) => {
        setError('');
    };

    useEffect(() => {
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);

        setError(''); // Reset validation error

        // Validate fields
        if (!name.trim()) {
            setError('Name is required.');
            setLoading(false);
            return;
        }

        if (!email.trim()) {
            setError('Email is required.');
            setLoading(false);
            return;
        }

        const user: User = {
            name,
            email,
            subject,
            phoneNumber,
            userType,
        };

        try {
            await axios.post('/api/users', user);
            setLoading(false);
            // You can reset the form here if needed
            setName('');
            setEmail('');
            setSubject('');
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
                <input type="text" className="form-control" placeholder="Intrested in subject" value={subject} onChange={(e) => setSubject(e.target.value)} />
            </div>
            <button type="submit" className="btn btn-primary" disabled={loading}>
                {loading ? 'Submitting...' : 'Submit'}
            </button>

            {error && <p className="mt-3 text-danger">{error}</p>}
        </form>
    );
};

export default UserForm;
