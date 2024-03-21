// TopUpPage.jsx
import React, { useState } from 'react';
import axios from 'axios';

const TopUpPage = ({ history }) => {
    const [amount, setAmount] = useState('');

    const handleTopUp = async (e) => {
        e.preventDefault();
        try {
            // Make API call to top up wallet balance using Axios
            await axios.post('api/wallet/top-up', { amount: parseFloat(amount) });
            // Redirect back to the user dashboard after successful top-up
            history.push('/dashboard');
        } catch (error) {
            console.error('Error topping up wallet:', error);
        }
    };

    return (
        <div>
            <h2>Top Up Wallet</h2>
            <form onSubmit={handleTopUp}>
                <label>
                    Amount:
                    <input 
                        type="number" 
                        value={amount} 
                        onChange={(e) => setAmount(e.target.value)} 
                        required 
                    />
                </label>
                <button type="submit">Top Up</button>
            </form>
        </div>
    );
};

export default TopUpPage;
