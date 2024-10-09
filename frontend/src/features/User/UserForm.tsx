import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface User {
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    phoneNumber: string;
    userType: string;
}

const UserForm: React.FC = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');

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
        if (!firstName.trim()) {
            setError('First Name is required.');
            setLoading(false);
            return;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            setError('Invalid email format.');
            setLoading(false);
            return;
        }

        const user: User = {
            firstName,
            lastName,
            email,
            subject,
            phoneNumber,
            userType,
        };
debugger;
        try {
            await axios.post('https://localhost:7256/userapi/createUser', user);
            setLoading(false);
            // You can reset the form here if needed
            setFirstName('');
            setLastName('');
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
            <div className="row">
                <div className="col-md-6 mb-3">
                    <input type="text" className="form-control" placeholder="First Name" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
                </div>
                <div className="col-md-6 mb-3">
                    <input type="text" className="form-control" placeholder="Last Name" value={lastName} onChange={(e) => setLastName(e.target.value)} />
                </div>
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
