import { UserProfile } from './user-profile';
import { SimpleClaim } from '../core/authentication/simple-claim';

export class AuthContext {
  userProfile: UserProfile;
  claims: SimpleClaim[];
}
