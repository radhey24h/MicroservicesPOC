
import { Link } from 'react-router-dom';
import logo from '../../assets/img/logo.png';

const Header = () => {

    return (
        <>
            <div className="row">
                <div className="col-12 d-flex justify-content-end align-items-center mb-3">
                    <div className="navigation-right">
                        <i className="icofont-phone" /> +91-989-977-4341
                        <i className="icofont-clock-time icofont-rotate-180" /> Wed-Sun: Opening at 6:30 AM
                    </div>
                </div>
            </div>

            <div className="row">
                <div className="col-12 d-flex justify-content-center align-items-center mb-3">
                    <div className="elementor-image">
                        <Link to="/">
                            <img src={logo} alt="Logo" />
                        </Link>
                    </div>
                </div>
            </div>

        </>
    );
};

export default Header;