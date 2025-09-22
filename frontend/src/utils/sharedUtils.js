export function getRiskClass(riskLevel) {
  const level = (riskLevel || '').toString().toLowerCase()
  return {
    'risk-high': level === 'high',
    'risk-medium': level === 'medium', 
    'risk-low': level === 'low'
  }
}