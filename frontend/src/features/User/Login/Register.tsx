import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'; // Import useNavigate


interface Login {
    password: string;
    email: string;
}

const Login: React.FC = () => {
    const navigate = useNavigate();
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
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

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(''); // Reset validation error


        if (!email.trim()) {
            setError('Email is required.');
            setLoading(false);
            return;
        }
        // Validate fields
        if (!password.trim()) {
            setError('Password is required.');
            setLoading(false);
            return;
        }

        const user: Login = {
            email,
            password,
        };

        navigate('/dashboard'); // Use navigate to redirect

        try {
            await axios.post('/api/login', user);
            setLoading(false);
            // You can reset the form here if needed
            setPassword('');
            setEmail('');
        } catch (err) {
            setError('Failed to submit the form. Please try again.');
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleLogin}>
            <div className="mb-3">
                <input type="email" className="form-control" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
            </div>
            <div className="mb-3">
                <input type="text" className="form-control" placeholder="Name" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <button type="submit" className="btn btn-primary" disabled={loading}>
                {loading ? 'Processing...' : 'Login'}
            </button>
            {error && <p className="mt-3 text-danger">{error}</p>}
        </form>
    );
};

export default Login;
