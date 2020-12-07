import { Offer } from './offer';

export interface Client {
  email: string,
  identityId: string,
  description: string,
  avatar: string,
  offers: Offer[]
}
