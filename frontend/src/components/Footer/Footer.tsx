
import { Link } from 'react-router-dom';

const Footer = () => {

    return (
        <>

            <div className="col-12 d-flex justify-content-center">
                <div className="social-links d-flex justify-content-around">
                    <a href="https://twitter.com/stepuplogics" target="_blank" rel="noreferrer" className="twitter mx-2">
                        <i className="fab fa-twitter" />
                    </a>
                    <a href="https://www.facebook.com/stepuplogics/" target="_blank" rel="noreferrer" className="facebook mx-2">
                        <i className="fab fa-facebook-f" />
                    </a>
                    <a href="https://www.instagram.com/stepuplogics/" target="_blank" rel="noreferrer" className="instagram mx-2">
                        <i className="fab fa-instagram" />
                    </a>
                    <a href="https://www.kooapp.com/profile/stepuplogics" target="_blank" rel="noreferrer" className="google-plus mx-2">
                        <i className="fab fa-google-plus-g" />
                    </a>
                    <a href="https://www.linkedin.com/company/stepup-logics" target="_blank" rel="noreferrer" className="linkedin mx-2">
                        <i className="fab fa-linkedin-in" />
                    </a>
                </div>
            </div>

        </>
    );
};

export default Footer;